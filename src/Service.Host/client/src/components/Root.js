﻿﻿import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Navigation, linnTheme } from '@linn-it/linn-form-components-library';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import MomentUtils from '@date-io/moment';
import ThemeProvider from '@material-ui/styles/ThemeProvider';
import PropTypes from 'prop-types';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import OutstandingWorksOrdersReport from '../containers/reports/OutstandingWorksOrdersReport';
import 'typeface-roboto';
import AteFaultCodes from '../containers/ate/AteFaultCodes';
import AteFaultCode from '../containers/ate/AteFaultCode';
import CreateAteFaultCode from '../containers/ate/CreateAteFaultCode';
import BuildsSummaryReportOptions from '../containers/buildsByDepartment/BuildsSummaryReportOptions';
import BuildsSummaryReport from '../containers/buildsByDepartment/BuildsSummaryReport';
import SerialNumberReissue from '../containers/serialNumberReissue/SerialNumberReissue';
import BuildsDetailReportOptions from '../containers/buildsByDepartment/BuildsDetailReportOptions';
import BuildsDetailReport from '../containers/buildsByDepartment/BuildsDetailReport';
import ManufacturingSkills from '../containers/manufacturingSkills/ManufacturingSkills';
import ManufacturingSkill from '../containers/manufacturingSkills/ManufacturingSkill';
import CreateManufacturingSkill from '../containers/manufacturingSkills/CreateManufacturingSkill';
import BoardFailTypes from '../containers/boardFailTypes/BoardFailTypes';
import BoardFailType from '../containers/boardFailTypes/BoardFailType';
import CreateBoardFailType from '../containers/boardFailTypes/CreateBoardFailType';
import AssemblyFailsWaitingListReport from '../containers/reports/AssemblyFailsWaitingListReport';
import AssemblyFail from '../containers/assemblyFails/AssemblyFail';
import CreateAssemblyFail from '../containers/assemblyFails/CreateAssemblyFail';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <ThemeProvider theme={linnTheme}>
                        <MuiPickersUtilsProvider utils={MomentUtils}>
                            <Router history={history}>
                                <div>
                                    <Navigation />
                                    <CssBaseline />

                                    <Route
                                        exact
                                        path="/"
                                        render={() => <Redirect to="/production/maintenance" />}
                                    />

                                    <Route
                                        exact
                                        path="/production"
                                        render={() => <Redirect to="/production/maintenance" />}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources"
                                        render={() => <Redirect to="/production/maintenance" />}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports"
                                        render={() => <Redirect to="/production/maintenance" />}
                                    />

                                    <Switch>
                                        <Route
                                            exact
                                            path="/production/maintenance"
                                            component={App}
                                        />
                                        <Route exact path="/production/quality" component={App} />
                                        <Route
                                            exact
                                            path="/production/quality/ate"
                                            component={App}
                                        />
                                        <Route
                                            exact
                                            path="/production/maintenance/works-orders"
                                            component={App}
                                        />

                                        <Route
                                            exact
                                            path="/production/maintenance/signin-oidc-client"
                                            component={Callback}
                                        />

                                        <Route
                                            exact
                                            path="/production/maintenance/works-orders/outstanding-works-orders-report"
                                            component={OutstandingWorksOrdersReport}
                                        />

                                        <Route
                                            exact
                                            path="/production/maintenance/serial-number-reissue"
                                            component={SerialNumberReissue}
                                        />

                                        <Route
                                            exact
                                            path="/production/quality/ate/fault-codes/create"
                                            component={CreateAteFaultCode}
                                        />
                                        <Route
                                            exact
                                            path="/production/quality/ate/fault-codes/:id"
                                            component={AteFaultCode}
                                        />
                                        <Route
                                            exact
                                            path="/production/quality/ate/fault-codes"
                                            component={AteFaultCodes}
                                        />
                                        <Route
                                            exact
                                            path="/production/reports/builds-summary-options"
                                            component={BuildsSummaryReportOptions}
                                        />
                                        <Route
                                            exact
                                            path="/production/reports/builds-summary"
                                            component={BuildsSummaryReport}
                                        />
                                        <Route
                                            exact
                                            path="/production/reports/builds-detail-options"
                                            component={BuildsDetailReportOptions}
                                        />
                                        <Route
                                            exact
                                            path="/production/reports/builds-detail"
                                            component={BuildsDetailReport}
                                        />
                                        <Route
                                            exact
                                            path="/production/resources/manufacturing-skills"
                                            component={ManufacturingSkills}
                                        />
                                        <Route
                                            exact
                                            path="/production/resources/manufacturing-skills/create"
                                            component={CreateManufacturingSkill}
                                        />
                                        <Route
                                            exact
                                            path="/production/resources/manufacturing-skills/:id"
                                            component={ManufacturingSkill}
                                        />
                                        <Route
                                            exact
                                            path="/production/resources/board-fail-types"
                                            component={BoardFailTypes}
                                        />
                                        <Route
                                            exact
                                            path="/production/resources/board-fail-types/create"
                                            component={CreateBoardFailType}
                                        />
                                        <Route
                                            exact
                                            path="/production/resources/board-fail-types/:id"
                                            component={BoardFailType}
                                        />
                                        <Route
                                            exact
                                            path="/production/reports/assembly-fails-waiting-list"
                                            component={AssemblyFailsWaitingListReport}
                                        />
                                        <Route
                                            exact
                                            path="/production/quality/assembly-fails/:id"
                                            component={AssemblyFail}
                                        />
                                        <Route
                                            exact
                                            path="/production/quality/create-assembly-fail"
                                            component={CreateAssemblyFail}
                                        />
                                    </Switch>
                                </div>
                            </Router>
                        </MuiPickersUtilsProvider>
                    </ThemeProvider>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
