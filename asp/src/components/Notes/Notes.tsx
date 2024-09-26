import React, { useEffect, useState } from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import { useNavigate } from "react-router-dom";
import axios from "axios";

interface NotesItem {
    id: string;
    title: string;
    text: string;
}

const NotesPage = () => {

    const navigateTo = useNavigate();

    const [noteList, setNoteList] = useState<NotesItem[]>([]);
       useEffect(() => {
        const token = localStorage.getItem("token");

        axios
            .get("https://localhost:7232/api/notes", {
                headers: { Authorization: `Bearer ${token}` }
            })
            .then((response) => {
                console.log("Полученные заметки:", response.data);
                setNoteList(response.data);
            })
            .catch((error) => {
                console.error("Ошибка при загрузке заметок:", error);
            });
    }, []);

    const removeNote = (noteId: string) => {
        const token = localStorage.getItem("token");

        axios
            .delete(`https://localhost:7232/api/notes/${noteId}`, {
                headers: { Authorization: `Bearer ${token}` }
            })
            .then((response) => {
                console.log("Заметка удалена:", response.data);
                setNoteList((prevNotes) => prevNotes.filter((note) => note.id !== noteId));
            })
            .catch((error) => {
                console.error("Ошибка при удалении заметки:", error);
            });
    };

    return (
        <div style={{ padding: "2rem" }}>
            <div style={{ textAlign: "right", marginBottom: "1rem" }}>
                <Button variant="success" onClick={() => navigateTo("/notes/new")}>
                    Добавить новую заметку
                </Button>
            </div>
            <div style={{ display: "flex", flexWrap: "wrap", gap: "1.5rem" }}>
                {noteList.map((note) => (
                    <Card key={note.id} style={{ width: "16rem" }}>
                        <Card.Body>
                            <Card.Title>{note.title}</Card.Title>
                            <Card.Text>{note.text}</Card.Text>
                            <Button
                                variant="primary"
                                className="me-2"
                                onClick={() => navigateTo(`/notes/${note.id}`)}
                            >
                                Редактировать
                            </Button>
                            <Button variant="danger" onClick={() => removeNote(note.id)}>
                                Удалить
                            </Button>
                        </Card.Body>
                    </Card>
                ))}
            </div>
        </div>
    );
};

export default NotesPage;
