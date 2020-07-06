import React, { Component } from 'react';
import ReactTable from 'react-table-6';
import 'react-table-6/react-table.css';
import { Button } from 'react-bootstrap';

export class PatientList extends Component {
    static displayName = PatientList.name;

    constructor(props) {
        super(props);
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
                Cell: props => {
                    return (
                        <Button variant="outline-primary" size="sm">Edit</Button>)
                },
                width: 100
            },
            {
                Header: '',
                Cell: props => {
                    return (
                        <Button variant="outline-info" size="sm">Report</Button>)
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
