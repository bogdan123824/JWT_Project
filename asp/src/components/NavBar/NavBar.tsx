import React, { useEffect, useState } from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";

function NavBar() {
    const [role, setRole] = useState<string>("");

    useEffect(() => {
        const storageRole = localStorage.getItem("role") || "";
        setRole(storageRole);
    }, []);

    return (
        <Navbar expand="lg" className="bg-body-tertiary">
            <Container>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link href="/login">Вход</Nav.Link>
                        <Nav.Link href="/register">Зарегистриоваться</Nav.Link>
                        {role === "Администратор" && (
                            <Nav.Link href="/users">Пользователи</Nav.Link>
                        )}
                        {(role === "Пользователь" || role === "Администратор") && (
                            <Nav.Link href="/notes">Заметки</Nav.Link>
                        )}
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default NavBar;
