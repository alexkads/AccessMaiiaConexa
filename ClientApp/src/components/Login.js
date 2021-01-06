import React, { useState } from 'react';
import styled from "styled-components";
import axios from "axios";

export const Container =  styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
`;

export const Input = styled.input`
    padding: 10px;
    width: 350px;
    height: 40px;
    border: none;
    background: #ecf0f1;
    border-radius: 3px;
    margin: 5px;
`;

export const Button = styled.button`
    padding: 10px;
    width: 360px;
    height: 50px;
    border: none;
    border-radius: 8px;
    background: ${props => props.primary ? "palevioletred" : "#9b59b6" };;
    color: #fff;
    margin: 10px;
    font-weight: bold;
`; 

export const Title = styled.p`
    font-weight: bold;
    color: #9b59b6;
    font-size: 22px;
`
export default function Login() {

  const [email, setEmail] = useState('');
  const [pass, setPass] = useState('');
  
  const handleLogin = () => {
      axios.post('users/login', { 'Username': email, 'Password': pass })
    .then(resp => console.log(resp))
    .catch(error => console.log(error));
  }
  
  const cadastro = () => {
    console.log(pass);
  }

  return (
    <Container> 
      <Title>Seja bem vindo, faça login para continuar </Title>
      <Input type="email" placeholder="Informe seu email"
      value={email} onChange={e=> setEmail(e.target.value)}
      />
      <Input type="password" placeholder="Informe sua senha"
      value={pass} onChange={e=> setPass(e.target.value)}
      />
      <Button onClick={handleLogin}> Logar </Button>
      {/* <Button primary onClick={cadastro}> Cadastre com e-mail agora </Button> */}
    </Container> 
  );
}
