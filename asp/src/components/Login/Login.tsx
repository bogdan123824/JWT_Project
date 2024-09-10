import React, { FC, useState } from 'react';
import { LoginWrapper, InputLogName, InputLogPass, SendLogBtn } from './Login.styled.ts';
import axios  from 'axios'


const Login = () => {
   const [login, setLogin] = useState("");
   const [pass, setPass] = useState("");

   function handleSubmit(event) {
      event.preventDefault();
      const loginPayload = {Username: login, Password: pass};
      try{
      axios.post("https://localhost:7231/api/authenticate/login", loginPayload).then((response) => {
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
   
   return(<LoginWrapper>
    <InputLogName placeholder='Введите имя' onChange={(e) => setLogin(e.target.value)}></InputLogName>
    
    <InputLogPass placeholder='Введите пароль' onChange={(e) => setPass(e.target.value)}></InputLogPass>

    <SendLogBtn onClick={handleSubmit}>Авторизироваться</SendLogBtn>
 </LoginWrapper>);
};

export default Login;
