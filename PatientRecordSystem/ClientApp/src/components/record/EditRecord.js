import React, { Component } from 'react';
import Select from 'react-select';
import axios from 'axios';
import {
    Container, Col, Form,
    FormGroup, Label, Input,
    Button,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { ToastContainer, toast, Zoom } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

export class EditRecord extends Component {

    constructor(props) {
        super(props);
        this.setDiseaseName = this.setDiseaseName.bind(this);
        this.setDescription = this.setDescription.bind(this);
        this.setTimeOfEntry = this.setTimeOfEntry.bind(this);
        this.setBill = this.setBill.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.validateEntries = this.validateEntries.bind(this);
        this.toastValidationErrors = this.toastValidationErrors.bind(this);
        this.state = {
            id: '',
            validationErrors: [],
            diseaseName: '',
            description: '',
            timeOfEntry: '',
            bill: '',
            patientId: '',
            patientName: ''
        }
    }

    componentDidMount() {
        const param = this.props.match.params;

        fetch(`/api/record/${param.id}`,
            {
                method: "GET"
            }).then(response => response.json()).then(record => {
                console.log(record);
                this.setState({
                    id: param.id,
                    diseaseName: record.diseaseName,
                    description: record.description,
                    timeOfEntry: record.timeOfEntry,
                    bill: record.bill,
                    patientId: record.patientId
                });
                fetch(`/api/patient/${record.patientId}`,
                    {
                        method: "GET"
                    }).then(response => response.json()).then(patient => {
                    this.setState({
                        patientName: patient.name
                    });
                });
            });
    }

    setDiseaseName(event) {
        this.setState({ diseaseName: event.target.value });
    }

    setDescription(event) {
        this.setState({ description: event.target.value });
    }

    setTimeOfEntry(event) {
        this.setState({ timeOfEntry: event.target.value });
    }

    setBill(event) {
        this.setState({ bill: event.target.value });
    }

    validateEntries() {
        if (this.state.diseaseName === '') {
            this.state.validationErrors.push("Disease name must be entered");
        }
        if (this.state.bill === '') {
            this.state.validationErrors.push("The amount of bill must be entered");
        }
        else if (isNaN(parseFloat(this.state.bill))) {
            this.state.validationErrors.push("The amount of bill must be entered in correct format");
        }
        if (this.state.timeOfEntry !== '' && isNaN(new Date(this.state.timeOfEntry))) {
            this.state.validationErrors.push("The time of entry should be in this format yyyy-mm-dd hh:mm");
        }

        return this.state.validationErrors.length > 0;
    }

    toastValidationErrors(item) {
        toast.error(item);
    }

    handleSubmit(event) {
        event.preventDefault();
        if (this.validateEntries()) {
            this.state.validationErrors.forEach(this.toastValidationErrors);
            this.setState({
                validationErrors: []
            });
        } else {
            axios.put('/api/record/',
                {
                        id: this.state.id,
                        diseaseName: this.state.diseaseName,
                        description: this.state.description,
                        timeOfEntry: this.state.timeOfEntry,
                        bill: this.state.bill,
                        patientId: this.state.patientId
                    })
                .then((response) => {
                        toast.success("Record saved");
                    },
                    (error) => {
                        console.log(error);
                        toast.error('The record was not saved');
                    });
        }
    }

    render() {

        const { diseaseName, description, timeOfEntry, bill } = this.state;

        return (
            <div>
                <>
                    <ToastContainer draggable={false} transition={Zoom} autoClose={5000}></ToastContainer>
                </>
                <Container>
                    <h3>Edit Record</h3> 
                    <Form className="form" onSubmit={this.handleSubmit}> 
                    <Col>
                    <FormGroup>
                    <Label><strong>Patient</strong></Label>
                                <Input
                                type="text"
                                name="patientName"
                                    id="patientName"
                                value={this.state.patientName}
                                disabled={true} />
                    </FormGroup>
                    </Col>
                    <Col>
                            <FormGroup>
                                <Label for="diseaseName"><strong>Disease name</strong></Label>
                                <Input
                                    type="text"
                                    name="diseaseName"
                                    id="diseaseName"
                                    value={diseaseName}
                                    onChange={this.setDiseaseName} />
                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <Label for="description"><strong>Description</strong></Label>
                                <Input
                                    type="text"
                                    name="description"
                                    id="description"
                                    value={description}
                                    onChange={this.setDescription} />
                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <Label for="timeOfEntry"><strong>Time of entry</strong></Label>
                                <Input
                                    type="text"
                                    name="timeOfEntry"
                                    id="timeOfEntry"
                                    value={timeOfEntry}
                                    onChange={this.setTimeOfEntry}
                                    placeholder="yyyy-mm-dd hh:mm" />
                            </FormGroup>
                        </Col>
                        <Col>
                            <FormGroup>
                                <Label for="bill"><strong>Bill</strong></Label>
                                <Input
                                    type="text"
                                    name="bill"
                                    id="bill"
                                    value={bill}
                                    onChange={this.setBill} />

                            </FormGroup>
                        </Col>
                        <Button type="Submit" id="btnSaveRecord" color="primary">Save</Button>
                        <Link to='/record-list'>
                            <Button color="info"> Cancel </Button>
                        </Link>
                    </Form> 
                </Container> 
            </div>
        )
    }
}