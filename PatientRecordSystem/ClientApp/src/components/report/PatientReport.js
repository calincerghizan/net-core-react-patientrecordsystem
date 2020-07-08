import React, { Component } from 'react';
import ReactTable from 'react-table-6';
import 'react-table-6/react-table.css';
import { ListGroup } from 'react-bootstrap';

export class PatientReport extends Component {

    constructor(props) {
        super(props);
        this.state = {
            patientReport: ''
        }
    }

    componentDidMount() {
        const param = this.props.match.params;

        fetch(`/api/report/patient/${param.id}`,
            {
                method: "GET"
            }).then(response => response.json()).then(patientReport => {
                console.log(patientReport);
                if (patientReport.billsAverageNoOutlier === 0) {
                    patientReport.billsAverageNoOutlier = patientReport.billsAverage;
                }
            this.setState({ patientReport: patientReport });
        });
    }

    render() {

        const hasBirthDateEntered = this.state.patientReport.age != null;
        const noRecords = this.state.patientReport.id === 0;
        const fifthRecordExists = this.state.patientReport.fifthRecord != null;

        const columns = [
            {
                Header: 'Id',
                accessor: 'id'
            },
            {
                Header: 'Patient name',
                accessor: 'name'
            },
            {
                Header: 'OfficialId',
                accessor: 'officialId'
            },
            {
                Header: 'Date of birth',
                accessor: 'dateOfBirth'
            },
            {
                Header: 'Email',
                accessor: 'email'
            }
        ];

        return (
            <div>
                <h3> Patient Report</h3 >
                {
                    noRecords ?
                        <h5>There are no records registered for this Patient</h5>
                    :
                        <ListGroup>
                            <ListGroup.Item>
                            Patient name <h5>{this.state.patientReport.name}</h5>
                            </ListGroup.Item>
                            {
                                hasBirthDateEntered ?
                                    <ListGroup.Item>
                                    Age <h5>{this.state.patientReport.age}</h5>
                                    </ListGroup.Item>
                                :
                                    <ListGroup.Item>
                                    Age <h5>The patient did not declare his birthday</h5>
                                    </ListGroup.Item>
                            }
                            <ListGroup.Item>
                            Average of bills <h5>{this.state.patientReport.billsAverage}</h5>
                            </ListGroup.Item>
                            <ListGroup.Item>
                                Average of bills removing outliers <h5>{this.state.patientReport.billsAverageNoOutlier}</h5>
                            </ListGroup.Item>
                            {
                                fifthRecordExists ?
                                    <ListGroup.Item>
                                        The 5th record
                                        <ListGroup>
                                            <ListGroup.Item>
                                                Disease name <h5>{this.state.patientReport.fifthRecord.diseaseName}</h5>
                                            </ListGroup.Item>
                                            <ListGroup.Item>
                                                Description <h5>{this.state.patientReport.fifthRecord.description}</h5>
                                            </ListGroup.Item>
                                            <ListGroup.Item>
                                                Time of entry <h5>{this.state.patientReport.fifthRecord.timeOfEntry}</h5>
                                            </ListGroup.Item>
                                            <ListGroup.Item>
                                                Bill <h5>{this.state.patientReport.fifthRecord.bill}</h5>
                                            </ListGroup.Item>
                                        </ListGroup> 
                                    </ListGroup.Item>
                                :
                                    <ListGroup.Item>
                                        The 5th record <h5>It does not exist</h5>
                                    </ListGroup.Item>
                            }
                            <ListGroup.Item>
                                Month with the highest number of visits <h5>{this.state.patientReport.monthWithMaxVisits}</h5>
                            </ListGroup.Item>
                            <ListGroup.Item>
                                List of patients with similar diseases (have in common at least 2 diseases)
                                <ReactTable data={this.state.patientReport.patientsWithSimilarDiseases} columns={columns} defaultPageSize={5} />
                            </ListGroup.Item>
                        </ListGroup>    
            }
            </div> 
    )
    }
}