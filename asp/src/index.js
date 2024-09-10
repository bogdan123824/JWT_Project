import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import "./index.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Routes, Route } from "react-router-dom";
import NavBar from "./components/NavBar/NavBar.tsx";
import Register from "./components/Register/Register.tsx";
import Login from "./components/Login/Login.tsx";
import Notes from "./components/Notes/Notes.tsx";
import FieldNote from "./components/FieldNote/FieldNote.tsx";
import UserComp from "./components/UserComp/UserComp.tsx";
import Container from "react-bootstrap/esm/Container";

ReactDOM.createRoot(document.getElementById("root")).render(
    <React.StrictMode>
        <BrowserRouter>
        <NavBar />
            <Container className="mt-2">
                <Routes>
                    <Route path="login" element={<Login />} />
                    <Route path="register" element={<Register />} />
                    <Route path="users" element={<UserComp />} />
                    <Route path="notes" element={<Notes />} />
                    <Route path="notes/:id" element={<FieldNote />} />
                    <Route path="notes/new" element={<FieldNote />} />
                </Routes>
            </Container>
        </BrowserRouter>
    </React.StrictMode>
);