import React, { Component } from 'react';
import axios from 'axios';
import {
    Container, Col, Row, Form,
    FormGroup, Label, Input,
    Button,
} from 'reactstrap';
import ReactTable from 'react-table-6';
import 'react-table-6/react-table.css';
import { Link } from 'react-router-dom';
import { ToastContainer, toast, Zoom } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

export class AddPatient extends Component {

    constructor(props) {
        super(props);
        this.clearSelection = this.clearSelection.bind(this);
        this.setName = this.setName.bind(this);
        this.setOfficialId = this.setOfficialId.bind(this);
        this.setDateOfBirth = this.setDateOfBirth.bind(this);
        this.setEmail = this.setEmail.bind(this);
        this.setKey = this.setKey.bind(this);
        this.setValue = this.setValue.bind(this);
        this.emailIsValid = this.emailIsValid.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.validateEntries = this.validateEntries.bind(this);
        this.toastValidationErrors = this.toastValidationErrors.bind(this);
        this.handleAddMetaDataClick = this.handleAddMetaDataClick.bind(this);
        this.state = {
            name: '',
            officialId: '',
            dateOfBirth: '',
            email: '',
            key: '',
            value: '',
            metaData: [],
            validationErrors: []
        }
    }

    setName(event) {
        this.setState({ name: event.target.value });
    }

    setOfficialId(event) {
        this.setState({ officialId: event.target.value });
    }

    setDateOfBirth(event) {
        this.setState({ dateOfBirth: event.target.value });
    }

    setEmail(event) {
        this.setState({ email: event.target.value });
    }

    setKey(event) {
        this.setState({ key: event.target.value });
    }

    setValue(event) {
        this.setState({ value: event.target.value });
    }

    clearSelection() {
        this.setState({
            name: '',
            officialId: '',
            dateOfBirth: '',
            email: ''
        });
    }

    emailIsValid(email) {
        const pattern = /[a-zA-Z0-9]+[\.]?([a-zA-Z0-9]+)?[\@][a-z]{3,9}[\.][a-z]{2,5}/g;
        const result = pattern.test(email);
        return result;
    }

    validateEntries() {
        if (this.state.name.trim() === '') {
            this.state.validationErrors.push("Patient Name must be entered");
        }
        if (this.state.officialId.trim() === '') {
            this.state.validationErrors.push("Official Id must be entered");
        }
        if (this.state.dateOfBirth !== '' && isNaN(new Date(this.state.dateOfBirth))) {
            this.state.validationErrors.push("Date Of Birth must be in format yyyy-mm-dd");
        }
        if (this.state.email !== '' && !this.emailIsValid(this.state.email)) {
            this.state.validationErrors.push("Email must have a valid format");
        }

        return this.state.validationErrors.length > 0;
    }

    toastValidationErrors(item) {
        toast.error(item);
    }

    handleAddMetaDataClick(event) {
        //event.preventDefault();
        if (this.state.key.trim() !== '' && this.state.value.trim() !== '') {
            const actualMetaData = this.state.metaData;
            actualMetaData.push({
                key: this.state.key,
                value: this.state.value
            });
            this.setState({ metaData: actualMetaData, key: '', value: '' });
        } else {
            toast.error("Meta Data must have both key and value");
        }
    }

    handleSubmit(event) {
        event.preventDefault();
        if (this.validateEntries()) {
            this.state.validationErrors.forEach(this.toastValidationErrors);
            this.setState({
                validationErrors: []
            });
        } else {

            axios.post('api/patient',
                    {
                        name: this.state.name,
                        officialId: this.state.officialId,
                        dateOfBirth: this.state.dateOfBirth,
                        email: this.state.email,
                        metaData: this.state.metaData
                    })
                .then((response) => {
                        toast.success("Patient saved");
                        this.clearSelection();
                        this.setState({ metaData: [] });
                    },
                    (error) => {
                        console.log(error);
                        toast.error(error.response.data);
                    });
        }
    }

    render() {

        const columns = [
            {
                Header: 'Key',
                accessor: 'key'
            },
            {
                Header: 'Value',
                accessor: 'value'
            }
        ];

        const { name, officialId, dateOfBirth, email, key, value } = this.state;

        return (

            <div>
                <>
                    <ToastContainer draggable={false} transition={Zoom} autoClose={5000}></ToastContainer>
                </>
                <Container>
                    <h3>Add Patient</h3>
                    <Form className="form" onSubmit={this.handleSubmit}> 
                        <Col>
                            <FormGroup>
                                <Label for="name"><strong>Patient name</strong></Label>
                                <Input
                                    type="text"
                                    name="name"
                                    id="name"
                                    value={name}
                                    onChange={this.setName} />
                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <Label for="officialId"><strong>Official id</strong></Label>
                                <Input
                                    type="text"
                                    name="officialId"
                                    id="officialId"
                                    value={officialId}
                                    onChange={this.setOfficialId} />
                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <Label for="dateOfBirth"><strong>Date of birth</strong></Label>
                                <Input
                                    type="text"
                                    name="dateOfBirth"
                                    id="dateOfBirth"
                                    value={dateOfBirth}
                                    onChange={this.setDateOfBirth}
                                    placeholder="yyyy-mm-dd" />
                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <Label for="email"><strong>Email address</strong></Label>
                                <Input
                                    type="text"
                                    name="email"
                                    id="email"
                                    value={email}
                                    onChange={this.setEmail} />

                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <Label><strong>Meta data</strong></Label>
                                <Container>
                                    <Col>
                                        <Row xs="3"> 
                                            <Col>
                                                <Label><strong>Key</strong></Label>
                                                <Input
                                                    type="text"
                                                    name="key"
                                                    id="key"
                                                    value={key}
                                                    onChange={this.setKey} />
                                            </Col>
                                            <Col>
                                                <Label><strong>Value</strong></Label>
                                                <Input
                                                    type="text"
                                                    name="value"
                                                    id="value"
                                                    value={value}
                                                    onChange={this.setValue} />
                                            </Col> 
                                            <Col>
                                                <Label> </Label>
                                                <Button type="button" id="btnAddMetaData" color="primary" onClick={this.handleAddMetaDataClick}>Add</Button>
                                            </Col> 
                                        </Row>
                                    </Col>
                                </Container>
                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <ReactTable data={this.state.metaData} columns={columns} defaultPageSize={5} />
                            </FormGroup>
                        </Col>
                        <Button type="Submit" id="btnSave" color="primary">Save</Button>
                        <Link to='/'>
                            <Button color="info"> Cancel </Button>
                        </Link>
                    </Form>
                </Container>
            </div>
        );
    }
}