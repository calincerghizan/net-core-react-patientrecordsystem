import React, { Component } from 'react';
import axios from 'axios';
import {
    Container, Col, Form,
    FormGroup, Label, Input,
    Button,
} from 'reactstrap';
import Select from 'react-select';

export class AddRecord extends Component {

    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {
            patients: []
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

    handleSubmit(event) {
        event.preventDefault();
        const formData = new FormData(event.target);
        axios.post('api/record',
                {
                    diseaseName: formData.get("diseaseName"),
                    description: formData.get("description"),
                    timeOfEntry: formData.get("timeOfEntry"),
                    bill: formData.get("bill"),
                    patientId: formData.get("id")
                })
            .then((response) => {
                    this.props.history.push('/record-list');
                },
                (error) => {
                    console.log(error);
                });
    }

    render() {
        return (
            <Container>
                <h3>Add Record</h3>
                <Form className="form" onSubmit={this.handleSubmit}>
                    <Col>
                        <FormGroup> 
                            <Label><strong>Patient</strong></Label> 
                            <Select id="id" name="id" options={this.state.patients} /> 
                </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                            <Label for="diseaseName"><strong>Disease name</strong></Label>
                            <Input
                                type="text"
                                name="diseaseName"
                                id="diseaseName"
                                required/>
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                            <Label for="description"><strong>Description</strong></Label>
                            <Input
                                type="text"
                                name="description"
                                id="description" />
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                            <Label for="timeOfEntry"><strong>Time of entry</strong></Label>
                            <Input
                                type="text"
                                name="timeOfEntry"
                                id="timeOfEntry" />
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                            <Label for="bill"><strong>Bill</strong></Label>
                            <Input
                                type="text"
                                name="bill"
                                id="bill" />
                        </FormGroup>
                    </Col>
                    <Button type="Submit" color="primary">Save</Button>
                </Form>
            </Container>
        );
    }
}