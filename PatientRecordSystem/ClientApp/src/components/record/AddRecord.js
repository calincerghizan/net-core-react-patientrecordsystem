import React, { Component } from 'react';
import axios from 'axios';
import {
    Container, Col, Form,
    FormGroup, Label, Input,
    Button,
} from 'reactstrap';
import Select from 'react-select';
import { ToastContainer, toast, Zoom, Bounce } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

export class AddRecord extends Component {

    constructor(props) {
        super(props);
        this.clearSelection = this.clearSelection.bind(this);
        this.setDiseaseName = this.setDiseaseName.bind(this);
        this.setDescription = this.setDescription.bind(this);
        this.setTimeOfEntry = this.setTimeOfEntry.bind(this);
        this.setBill = this.setBill.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.validateEntries = this.validateEntries.bind(this);
        this.toastValidationErrors = this.toastValidationErrors.bind(this);
        this.state = {
            patients: [],
            validationErrors: [],
            patient: null,
            diseaseName: '',
            description: '',
            timeOfEntry: '',
            bill: ''
        }
    }

    componentDidMount() {

        fetch('api/patient')
            .then(response => {
                return response.json();
            }).then(data => {
                let patientsFromApi = data.map((patient) => {
                    return { value: patient.id, label: patient.name };
                });
                this.setState({
                    patients: patientsFromApi
            });
            }).catch(error => {
                console.log(error);
            });
    }

    patient = (selectedPatient) => {
        this.setState({ patient: selectedPatient });
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

    clearSelection() {
        this.setState({
            patient: null,
            diseaseName: '',
            description: '',
            timeOfEntry: '',
            bill: ''
        });
    }

    validateEntries() {
        if (this.state.patient == null) {
            this.state.validationErrors.push("A patient must be selected");
        }
        if (this.state.diseaseName === '') {
            this.state.validationErrors.push("Disease name must be entered");
        }
        if (this.state.bill === '') {
            this.state.validationErrors.push("The amount of bill must be entered");
        }
        else if (isNaN(parseFloat(this.state.bill))) {
            this.state.validationErrors.push("The amount of bill must be entered in correct format");
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
            this.state.validationErrors = [];
        } else {

            axios.post('api/record',
                    {
                        diseaseName: this.state.diseaseName,
                        description: this.state.description,
                        timeOfEntry: this.state.timeOfEntry,
                        bill: this.state.bill,
                        patientId: this.state.patient.value
                    })
                .then((response) => {
                        toast.success("Record saved");
                        this.clearSelection();
                    },
                    (error) => {
                        console.log(error);
                        toast.error('The record was not saved');
                    });
        }
    }

    render() {

        const { patient, diseaseName, description, timeOfEntry, bill } = this.state;

        return (

            <div>
                <>
                    <ToastContainer draggable={false} transition={Zoom} autoClose={5000}></ToastContainer>
                </>
            <Container>
                <h3>Add Record</h3>
                <Form className="form" id="addRecordForm" onSubmit={this.handleSubmit}>
                    <Col>
                        <FormGroup> 
                            <Label><strong>Patient</strong></Label> 
                                <Select id="id" name="id" options={this.state.patients} placeholder="Select a patient" value={patient} onChange={this.patient}/> 
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
                                onChange={this.setDescription}   />
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
                                onChange={this.setTimeOfEntry} />
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
                    <Button type="Submit" color="primary">Save</Button>
                </Form>
                </Container>
            </div>
        );
    }
}