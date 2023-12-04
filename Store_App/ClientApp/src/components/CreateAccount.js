import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import {
    Alert,
    Input,
    Label,
    Form,
    FormGroup,
    FormFeedback,
    Button,
    Card,
    CardBody,
    CardTitle
} from 'reactstrap'

export default function CreateAccount() {
    const emailRegex = /^[^@\s@]+@[^@\s]+\.[^@\s]+$/
    const passRegex = /^[^\s]{8,128}$/

    const [fields, setFields] = useState({})
    const [validation, setValidation] = useState({})
    const [status, setStatus] = useState(0)
    const navigate = useNavigate()

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

    const handleChange = (field, value) => {
        setFields({
            ...fields,
            [field]: value
        })
    }

    const HandleSubmit = async event => {
        event.preventDefault();
        const URIEmail = encodeURIComponent(fields["email"])
        const URIPassword = encodeURIComponent(fields["password"])

        try {
            const response = await fetch(`api/account/createaccount?email=${URIEmail}&password=${URIPassword}`)
            if (response.ok) {
                const token = await response.text()
                localStorage.setItem("authtoken", token)
                navigate("/")
                window.location.reload()
            } else if (response.status === 400) {
                setStatus(400)
            } else if (response.status === 403) {
                setStatus(403)
            } else {
                setStatus(500)
            }
        }
        catch (Exception) {
            setStatus(500)
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
        <div className="d-flex flex-wrap justify-content-center" style={{ gridColumnGap: "100%" }}>
            <Card style={{ width: '20rem' }}>
                <CardBody>
                    <CardTitle tag="h4" style={{ textAlign: "center" }}>Create Account</CardTitle><br></br>
                    <Form onSubmit={HandleSubmit}>
                        <Alert color="danger" isOpen={status === 400}> Form input is invalid. </Alert>
                        <Alert color="danger" isOpen={status === 403}> That email address is already associated with an account. </Alert>
                        <Alert color="warning" isOpen={status === 500}> There was an error while attempting to create an account. </Alert>
                        {' '}
                        <FormGroup>
                            <Label id="email">Email</Label>
                            <Input id="email" type="email" required value={fields["email"]} onChange={e => handleChange("email", e.target.value)} invalid={validation["email"] === 0} />                          
                            <FormFeedback invalid>A valid email address is required.</FormFeedback>
                        </FormGroup>
                        {' '}
                        <FormGroup>
                            <Label id="password">Password</Label>
                            <Input id="password" type="password" required value={fields["password"]} onChange={e => handleChange("password", e.target.value)} invalid={validation["password"] === 0} />                           
                            <FormFeedback invalid>Passwords must contain at least 8 characters and no whitespace.</FormFeedback>
                        </FormGroup>
                        {' '}
                        <FormGroup>
                            <Label id="password2">Confirm Password</Label>
                            <Input id="password2" type="password" required value={fields["password2"]} onChange={e => handleChange("password2", e.target.value)} invalid={validation["password2"] === 0} />
                            <FormFeedback invalid>Passwords do not match.</FormFeedback>
                        </FormGroup>
                        {' '}
                        <FormGroup>
                            <Button block type="submit" color="login" disabled={validForm === false}>Create Account</Button>
                        </FormGroup>
                    </Form>
                </CardBody>
            </Card>
        </div>
    )
}