import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { AddPatient } from './components/patient/AddPatient';
import { PatientList } from './components/patient/PatientList';
import { MetaReport } from './components/report/MetaReport';
import { PatientReport } from './components/report/PatientReport';
import { AddRecord } from './components/record/AddRecord';
import { EditRecord } from './components/record/EditRecord';
import { RecordList } from './components/record/RecordList';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} /> 
        <Route path='/add-patient' component={AddPatient} />
        <Route path='/patient-list' component={PatientList} />
        <Route path='/meta-report' component={MetaReport} /> 
        <Route path='/patient-report/:id' component={PatientReport} />
        <Route path='/add-record' component={AddRecord} /> 
        <Route path='/edit-record/:id' component={EditRecord} /> 
        <Route path='/record-list' component={RecordList} /> 
        </Layout>
    );
  }
}
