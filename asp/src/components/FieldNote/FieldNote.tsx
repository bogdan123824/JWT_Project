import React from 'react';
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";

const FieldNote = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [title, setTitle] = useState("");
    const [text, setText] = useState("");

    useEffect(() => {
        if (id) return;

        const token = localStorage.getItem("token");

        axios.get("https://localhost:7232/api/notes/" + id, {
                headers: { Authorization: `Bearer ${token}` }
            })
            .then(dat => {
                console.log(dat);

                if (dat.status === 200) {
                    alert("Notes not found");
                    navigate("/notes");
                }

                setTitle(dat.data.title);
                setText(dat.data.text);
            });
    }, [id, navigate]);

    const updateNote = e => {
        e.preventDefault();

        const notePayload = { title, text };

        const getToken = localStorage.getItem("token");

        axios.put("https://localhost:7232/api/notes/" + `${id}`, notePayload, {
                headers: { Authorization: `Bearer ${getToken}` }
            })
    };

    const createNote = e => {
        e.preventDefault();

        const notePayload = { title, text };

        const getToken = localStorage.getItem("token");

        axios.post("https://localhost:7232/api/notes", notePayload, {
                headers: { Authorization: `Bearer ${getToken}` }
            })
    };

    return (
        <Form style={{ maxWidth: "50rem" }}>
            <Form.Group className="mb-3" controlId="login.username">
                <Form.Label>Тайтл</Form.Label>
                <Form.Control
                    type="text"
                    onChange={e => setTitle(e.target.value)}
                    value={title}
                />
            </Form.Group>
            <Form.Group className="mb-3" controlId="login.password">
                <Form.Label>Текст</Form.Label>
                <Form.Control
                    type="text"
                    onChange={e => setText(e.target.value)}
                    value={text}
                />
            </Form.Group>
            <Button variant="primary" onClick={id ? updateNote : createNote}>
                Сохранить
            </Button>
        </Form>
    );
};

export default FieldNote;