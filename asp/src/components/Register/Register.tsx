import React, { FC, useState } from 'react';
import axios from 'axios';
import { RegisterWrapper,InputEmail, InputName, InputPass,SendBtn } from './Register.styled.ts';

const Register  = () => {
   const [email, setEmail] = useState("");
   const [login, setLogin] = useState("");
   const [pass, setPass] = useState("");

   function handleSubmit(event) {
      event.preventDefault();
      const loginPayload = {Username: login, Email: email, Password: pass};
      try{
      axios.post("https://localhost:7231/api/authenticate/register", loginPayload).then((response) => {
         if (response.status != 200)
         {
            alert(response.status);
         }

      }).catch( (e) => console.log(e));
   }
   catch (e) {
      alert(e);
   }
   }
   
   return (<RegisterWrapper>
    <InputEmail placeholder='Введите почту' onChange={(e) => setEmail(e.target.value)}></InputEmail>
    
    <InputName placeholder='Введите имя' onChange={(e) => setLogin(e.target.value)}></InputName>
    
    <InputPass placeholder='Введите пароль' onChange={(e) => setPass(e.target.value)}></InputPass>

    <SendBtn onClick={handleSubmit}>Зарегестрироваться</SendBtn>
 </RegisterWrapper>);
};

export default Register;
