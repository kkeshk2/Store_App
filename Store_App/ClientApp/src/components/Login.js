import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import {
    Alert,
    Input,
    Label,
    Form,
    FormGroup,
    FormText,
    FormFeedback,
    Row,
    Col,
    Button,
    Nav,
    NavItem,
    NavLink
} from 'reactstrap'

export default function Login() {

    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [status, setStatus] = useState(0)
    const navigate = useNavigate();

    useEffect(() => {

        const verifyUser = async () => {
            if (localStorage.getItem("authtoken")) {
                try {
                    const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                    const response = await fetch(`api/account/verifyaccount`, { headers });
                    if (!response.ok) {
                        localStorage.removeItem("authtoken")
                    }
                } catch (Exception) {
                    localStorage.removeItem("authtoken")
                }
            }

            if (localStorage.getItem("authtoken")) {
                navigate("/")
                window.location.reload()
            }
        }

        verifyUser();
    }, []);

    const HandleSubmit = async event => {
        event.preventDefault();
        const URIEmail = encodeURIComponent(email)
        const URIPassword = encodeURIComponent(password)

        try {
            const response = await fetch(`api/account/login?email=${URIEmail}&password=${URIPassword}`)
            if (response.ok) {
                const token = await response.text()
                localStorage.setItem("authtoken", token)
                navigate("/")
                window.location.reload()
            } else if (response.status === 400) {
                setStatus(400)
            } else {
                setStatus(502)
            }
        }
        catch (Exception) {
            setStatus(502)
        }
    }

    const HandleEmail = (event) => {
        setEmail(event.target.value)
    }

    const HandlePassword = (event) => {
        setPassword(event.target.value)
    }

    const HandleRedirect = (event) => {
        navigate("/")
    }

    return (
        <Form id="login" onSubmit={HandleSubmit}>
            <Row><Col lg="5" style={{ textAlign: "center" }}>
                <h2>Log In</h2>
            </Col></Row>
            <Row><Col lg="5">
                <Alert color="danger" isOpen={status === 400}> Your email or password is incorrect. </Alert>
                <Alert color="warning" isOpen={status === 502}> There was an error logging in. </Alert>
            </Col></Row>
            <Row><Col lg="5">
                <FormGroup>
                    <Input id="email" type="email" placeholder="Email" required value={email} onChange={HandleEmail}/>
                </FormGroup>
            </Col></Row>
            <Row><Col lg="5">
                <FormGroup>
                    <Input id="password" type="password" placeholder="Password" required value={password} onChange={HandlePassword}/>
                </FormGroup>
            </Col></Row>
            <Row><Col lg="5">
                <FormGroup>
                    <Button block type="submit" color="login"> Log In </Button>
                </FormGroup>
            </Col></Row>
            <Row><Col lg="5">
                <Nav justified>
                    <NavLink href="/"> Forgot Password </NavLink>                     
                </Nav>
            </Col></Row>
            <Row><Col lg="5">
                <Nav  justified>
                    <NavLink href="/create-account"> Create Account </NavLink>
                </Nav>
            </Col></Row>
        </Form>
    )
}