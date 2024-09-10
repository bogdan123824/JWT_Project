// Notes.styled.js
import styled from "styled-components";

export const NotesContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  margin-top: 4.5rem;
  margin-bottom: 2rem;
`;

export const NoteCard = styled.div`
  width: 15rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  background-color: #f9f9f9;
  padding: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
`;

export const NoteTitle = styled.h2`
  font-size: 1.5rem;
  margin-bottom: 10px;
  color: #333;
`;

export const NoteDescription = styled.p`
  font-size: 1rem;
  color: #555;
`;

export const ButtonContainer = styled.div`
  margin-top: 1rem;
  display: flex;
  justify-content: space-between;
`;

// Статичная стилизация для кнопок
export const WarningButton = styled.button`
  background-color: #f0ad4e;
  color: white;
  padding: 0.375rem 0.75rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;

  &:hover {
    background-color: #ec971f;
  }
`;

export const DangerButton = styled.button`
  background-color: #d9534f;
  color: white;
  padding: 0.375rem 0.75rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;

  &:hover {
    background-color: #c9302c;
  }
`;

export const AddNoteButton = styled.button`
  position: absolute;
  top: 4rem;
  right: 5rem;
  background-color: #5cb85c;
  color: white;
  padding: 0.375rem 0.75rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;

  &:hover {
    background-color: #4cae4c;
  }
`;
