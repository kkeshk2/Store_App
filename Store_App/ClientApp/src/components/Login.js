import React, { Component, useState, useEffect } from 'react';

import {
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

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    async function handleSubmit() {
    }
   
    return (
        <div>
            <Form>
                <Row>
                    <Col lg="6">
                        <FormGroup>
                            <Label for="email"> Email </Label>
                            <Input id="email" type="email" value={email} onChange={e => setEmail(e.target.value)} required></Input>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col lg="6">
                        <FormGroup>
                            <Label for="password"> Password </Label>
                            <Input id="password" type="password" value={password} onChange={e => setPassword(e.target.value)}></Input>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col lg="6">
                        <FormGroup>
                            <Button block type="submit" onClick={handleSubmit}>Log In</Button>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col lg="6">
                        <Nav justified>
                            <NavItem>
                                <NavLink href="/">Forgot Password</NavLink>
                            </NavItem>                     
                            <NavItem>
                                <NavLink href="/">Create Account</NavLink>
                            </NavItem>
                        </Nav>
                    </Col>
                </Row>
                <Row>
                    <Col lg="6">
                        
                    </Col>
                </Row>
            </Form>
        </div>
    )
}