import React, { useEffect, useState } from "react";
import axios from "axios";
import ListGroup from "react-bootstrap/ListGroup";
import Button from "react-bootstrap/Button";

interface User {
    id: number;
    username: string;
    role: string;
}

const UsersDisplay: React.FC = () => {
    const [userList, setUserList] = useState<User[]>([]);

    useEffect(() => {
        const token = localStorage.getItem("token");

        axios
            .get("https://localhost:7232/api/users", {
                headers: { Authorization: `Bearer ${token}` }
            })
            .then(response => {
                console.log("Список пользователей:", response.data);
                setUserList(response.data);
            })
            .catch(error => {
                console.error("Ошибка при загрузке пользователей:", error);
            });
    }, []);

    const userRole = (userId: number, newRole: string) => {
        const token = localStorage.getItem("token");
        const currentUser = userList.find(user => user.id === userId);

        if (!currentUser) {
            alert("Пользователь не найден");
            return;
        }

        const updateUser = {
            username: currentUser.username,
            role: newRole
        };

        axios
            .put(`https://localhost:7232/api/users/${userId}`, updateUser, {
                headers: { Authorization: `Bearer ${token}` }
            })
            .then(response => {
                console.log("Роль пользователя обновлена:", response.data);
                setUserList(prevList =>
                    prevList.map(user =>
                        user.id === userId ? { ...user, role: newRole } : user
                    )
                );
            })
            .catch(error => {
                console.error("Ошибка при обновлении роли пользователя:", error);
            });
    };

    return (
        <ListGroup as="ul">
            {userList.map(user => (
                <ListGroup.Item key={user.id} className="d-flex justify-content-between align-items-center">
                    <div className="user-info">
                        <strong>{user.username}</strong>
                        <span className="user-role" style={{ marginLeft: "10px", fontStyle: "italic" }}>
                            ({user.role})
                        </span>
                    </div>
                    <Button
                        variant={user.role === "Администратор" ? "danger" : "primary"}
                        onClick={() =>
                            userRole(
                                user.id,
                                user.role === "Администратор" ? "Пользователь" : "Администратор"
                            )
                        }
                    >
                        {user.role === "Администратор" ? "Понизить до пользователя" : "Повысить до администратора"}
                    </Button>
                </ListGroup.Item>
            ))}
        </ListGroup>
    );
};

export default UsersDisplay;
