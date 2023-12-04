import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import {
    Alert,
    Input,
    Label,
    Form,
    FormGroup,
    Row,
    Col,
    Button,
    Nav,
    NavLink,
    Card,
    CardBody,
    CardTitle,
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
            } else if (response.status === 401) {
                setStatus(401)
            } else {
                setStatus(500)
            }
        }
        catch (Exception) {
            setStatus(500)
        }
    }

    const HandleEmail = (event) => {
        setEmail(event.target.value)
    }

    const HandlePassword = (event) => {
        setPassword(event.target.value)
    }

    return (
        <div className="d-flex flex-wrap justify-content-center" style={{ gridColumnGap: "100%" }} >
            <Card style={{ maxWidth: '20rem', width: '20rem' }}>
                <CardBody>
                    <Form id="login" onSubmit={HandleSubmit}>
                        <Row><Col style={{ textAlign: "center" }}>
                            <CardTitle tag="h4">Log In</CardTitle>
                        </Col></Row>
                        <br />
                        <Row><Col>
                            <Alert color="danger" isOpen={status === 401}> Your email or password is incorrect. </Alert>
                            <Alert color="warning" isOpen={status === 500}> There was an error logging in. </Alert>
                        </Col></Row>
                        <Row><Col>
                            <FormGroup>
                                <Label for="email">Email</Label>
                                <Input id="email" type="email" required value={email} onChange={HandleEmail} />
                            </FormGroup>
                        </Col></Row>
                        <Row><Col>
                            <FormGroup>
                                <Label for="password">Password</Label>
                                <Input id="password" type="password" required value={password} onChange={HandlePassword} />
                            </FormGroup>
                        </Col></Row>
                        <Row><Col>
                            <FormGroup>
                                <Button block type="submit" color="login"> Log In </Button>
                            </FormGroup>
                        </Col></Row>
                        <Row><Col>
                            <Nav justified>
                                <NavLink href="/create-account"> Create Account </NavLink>
                            </Nav>
                        </Col></Row>
                    </Form>
                </CardBody>
            </Card>
        </div>
    )
}