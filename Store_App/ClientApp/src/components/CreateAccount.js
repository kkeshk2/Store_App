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
    NavLink,
    Card,
    CardBody,
    CardTitle
} from 'reactstrap'

export default function CreateAccount() {
    const emailRegex = /^[^@\s@]+@[^@\s]+\.[^@\s]+$/
    const passRegex = /^[^\s]{8,128}$/
    const nameRegex = /^[A-Za-z][A-Za-z\.\-\x20]{0,127}$/

    const [fields, setFields] = useState({})
    const [validation, setValidation] = useState({})
    const [status, setStatus] = useState(0)
    const navigate = useNavigate()

    const handleChange = (field, value) => {
        setFields({
            ...fields,
            [field]: value
        })
    }

    const HandleSubmit = async event => {
        event.preventDefault();
        const URIName = encodeURIComponent(fields["name"])
        const URIEmail = encodeURIComponent(fields["email"])
        const URIPassword = encodeURIComponent(fields["password"])

        try {
            const response = await fetch(`api/account/createaccount?email=${URIEmail}&password=${URIPassword}&name=${URIName}`)
            if (response.ok) {
                const token = await response.text()
                localStorage.setItem("authtoken", token)
                navigate("/")
                window.location.reload()
            } else if (response.status === 400) {
                setStatus(400)
            } else if (response.status === 401) {
                setStatus(401)
            } else {
                setStatus(502)
            }
        }
        catch (Exception) {
            setStatus(502)
        }
    }

    let validForm = true;

    if (fields["email"] != null) {
        if (emailRegex.test(fields["email"])) {
            validation["email"] = 1;
        } else {
            validation["email"] = 0;
            validForm = false;
        }
    }

    if (fields["name"] != null) {
        if (nameRegex.test(fields["name"])) {
            validation["name"] = 1;
        } else {
            validation["name"] = 0;
            validForm = false;
        }
    }

    if (fields["password"] != null) {
        if (passRegex.test(fields["password"])) {
            validation["password"] = 1;
        } else {
            validation["password"] = 0;
            validForm = false;
        }
    }

    if (fields["password2"] != null) {
        if (fields["password"] === fields["password2"]) {
            validation["password2"] = 1;
        } else {
            validation["password2"] = 0;
            validForm = false;
        }
    }

    return (
        <Card style={{width: '20rem'}}>
            <CardBody>
                <CardTitle tag="h3" text>Create Account</CardTitle><br></br>
                <Form onSubmit={HandleSubmit}>
                    <Alert color="danger" isOpen={status === 400}> Form input is invalid. </Alert>
                    <Alert color="danger" isOpen={status === 401}> That email address is already associated with an account. </Alert>
                    <Alert color="warning" isOpen={status === 502}> There was an error while attempting to create an account. </Alert>
                    {' '}
                    <FormGroup floating>
                        <Input id="name" placeholder="Name" required value={fields["name"]} onChange={e => handleChange("name", e.target.value)} invalid={validation["name"] === 0} />
                        <Label id="name">Name</Label>
                        <FormFeedback invalid s>Names must start with a letter.<br></br><br></br> Names may not contain numbers or special characters except "." and "-".</FormFeedback>
                    </FormGroup>
                    {' '}
                    <FormGroup floating>
                        <Input id="email" type="email" placeholder="Email" required value={fields["email"]} onChange={e => handleChange("email", e.target.value)} invalid={validation["email"] === 0} />
                        <Label id="email">Email</Label>
                        <FormFeedback invalid>A valid email address is required.</FormFeedback>
                    </FormGroup>
                    {' '}
                    <FormGroup floating>
                        <Input id="password" type="password" placeholder="Password" required value={fields["password"]} onChange={e => handleChange("password", e.target.value)} invalid={validation["password"] === 0} />
                        <Label id="password">Password</Label>
                        <FormFeedback invalid>Passwords must contain at least 8 characters and no whitespace.</FormFeedback>
                    </FormGroup>
                    {' '}
                    <FormGroup floating>
                        <Input id="password2" type="password" placeholder="Confirm Password" required value={fields["password2"]} onChange={e => handleChange("password2", e.target.value)} invalid={validation["password2"] === 0} />
                        <Label id="password2">Confirm Password</Label>
                        <FormFeedback invalid>Passwords do not match.</FormFeedback>
                    </FormGroup>
                    {' '}
                    <FormGroup>
                        <Button block type="submit" color="login" disabled={validForm === false}>Create Account</Button>
                    </FormGroup>
                </Form>
            </CardBody>
        </Card>
    )
}