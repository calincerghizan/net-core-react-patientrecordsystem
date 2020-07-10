import React, { Component } from 'react';
import ReactTable from 'react-table-6';
import 'react-table-6/react-table.css';
import { Button } from 'react-bootstrap';

export class PatientList extends Component {

    constructor(props) {
        super(props);
        this.onEditClick = this.onEditClick.bind(this);
        this.onReportClick = this.onReportClick.bind(this);
        this.state = {
            patients: []
        }
    }

    componentDidMount() {
        const url = "/api/patient";
        fetch(url,
            {
                method: "GET"
            }).then(response => response.json()).then(patients => {
            this.setState({ patients: patients });
        });
    }

    onEditClick(patient) {
        const location = {
            pathname: '/edit-patient/' + patient.id
        };
        this.props.history.push(location);
    }

    onReportClick(patient) {
        const location = {
            pathname: '/patient-report/' + patient.id
        };
        this.props.history.push(location);
    }

    render() {
        const columns = [
            {
                Header: 'Patient name',
                accessor: 'name'
            },
            {
                Header: 'Date of birth',
                accessor: 'dateOfBirth'
            },
            {
                Header: 'Last entry',
                accessor: 'lastEntry'
            },
            {
                Header: 'MetaData count',
                accessor: 'metaDataCount'
            },
            {
                Header: '',
                Cell: ({ row }) => {
                    return (
                        <Button variant="outline-primary" size="sm" onClick={(e) => this.onEditClick(row._original)}>Edit</Button>)
                },
                width: 100
            },
            {
                Header: '',
                Cell: ({ row }) => {
                    return (
                        <Button variant="outline-primary" size="sm" onClick={(e) => this.onReportClick(row._original)}>Report</Button>)
                },
                width: 100
            }
        ];

        return (
            <div>
                <h3>Patient List</h3>
                <ReactTable data={this.state.patients} columns={columns} defaultPageSize= { 5 } />
            </div>
    )
    }
}
