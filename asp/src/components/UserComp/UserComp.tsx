import React, { useEffect, useState } from "react";
import axios from "axios";
import ListGroup from "react-bootstrap/ListGroup";
import Button from "react-bootstrap/Button";

interface User {
    id: number;
    username: string;
    role: string;
}

const UsersComponent = () => {
    const [users, setUsers] = useState<User[]>([]);

    useEffect(() => {
        const getToken = localStorage.getItem("token");

        axios
            .get("https://localhost:7232/api/" + "users", {
                headers: { Authorization: `Bearer ${getToken}` }
            })
            .then(dat => {
                console.log(dat);
                setUsers(dat.data);
            });
    }, []);

    const changeRole = (userId: number, newRole: string) => {

        const getToken = localStorage.getItem("token");

        const user = users.find((x: User) => x.id === userId);

        if (!user) {
            alert("Пользователь не найден");
            return;
        }

        const userPayload = {
            username: user.username,
            role: newRole
        };

        axios
            .put("https://localhost:7232/api/users/" + userId, userPayload, {
                headers: { Authorization: `Bearer ${getToken}` }
            })
            .then(dat => {
                console.log(dat);
            });
    };

    return (
        <ListGroup as="ul">
            {users.map(user => (
                <ListGroup.Item
                    as="li"
                    key={user.username}
                    className="d-flex justify-content-between align-items-start">
                    <div className="ms-2 me-auto" style={{ height: "4rem" }}>
                        <div className="fw-bold" style={{ fontSize: "1.2rem" }}>
                            {user.username}
                        </div>
                        <Button
                            variant="warning"
                            onClick={() =>
                                changeRole(
                                    user.id,
                                    user.role === "Администратор" ? "Пользователь" : "Администратор"
                                )
                            }
                            style={{
                                position: "absolute",
                                right: "1rem",
                                fontSize: "1rem"
                            }}>
                            {user.role === "Администратор"
                                ? "Понизить до пользователя"
                                : "Повысить до Администратора"}
                        </Button>
                    </div>
                </ListGroup.Item>
            ))}
        </ListGroup>
    );
};

export default UsersComponent;
