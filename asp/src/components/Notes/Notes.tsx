import React, { useEffect, useState } from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import { useNavigate } from "react-router-dom";
import axios from "axios";

interface Note {
    id: string;
    title: string;
    text: string; 
}

const Notes = () => {
    const navigate = useNavigate();
    const [items, setItems] = useState<Note[]>([]); 

    useEffect(() => {
        const getToken = localStorage.getItem("token");

        axios
            .get("https://localhost:7232/api/notes", {
                headers: { Authorization: `Bearer ${getToken}` }
            })
            .then((dat) => {
                console.log(dat);
                setItems(dat.data); 
            });
    }, []);

    const deleteNote = (id: string) => {
        const getToken = localStorage.getItem("token");

        axios
            .delete(`https://localhost:7232/api/notes/${id}`, {
                headers: { Authorization: `Bearer ${getToken}` }
            })
            .then((res) => {
                console.log(res);
                setItems((items) => items.filter((item) => item.id !== id));
            });
    };

    return (
        <>
            <div
                style={{
                    position: "absolute",
                    top: "4rem",
                    right: "5rem"
                }}>
                <Button variant="success" onClick={() => navigate("/notes/new")}>
                    Добавить 
                </Button>
            </div>
                <div
                    style={{
                        display: "flex",
                        marginTop: "4.5rem",
                        marginBottom: "2rem"
                    }}>
                    {items.map((item) => (
                        <Card key={item.id} style={{ width: "15rem" }}>
                            <Card.Body>
                                <Card.Title>{item.title}</Card.Title>
                                <Card.Text>{item.text}</Card.Text> 
                                <Button
                                    variant="warning"
                                    className="me-1"
                                    onClick={() => navigate(`/notes/${item.id}`)}>
                                    Изменить 
                                </Button>
                                <Button
                                    variant="danger"
                                    className="me-1"
                                    onClick={() => deleteNote(item.id)}>
                                    Удалить
                                </Button>
                            </Card.Body>
                        </Card>
                    ))}
                </div>
        </>
    );
};

export default Notes;
