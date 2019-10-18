﻿import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Navigation } from '@linn-it/linn-form-components-library';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import MomentUtils from '@date-io/moment';
import PropTypes from 'prop-types';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import OutstandingWorksOrdersReport from '../containers/reports/OutstandingWorksOrdersReport';
import OutstandingWorksOrdersReportOptions from '../containers/reports/OutstandingWorksOrdersReportOptions';
import 'typeface-roboto';
import AteFaultCodes from '../containers/ate/AteFaultCodes';
import AteFaultCode from '../containers/ate/AteFaultCode';
import CreateAteFaultCode from '../containers/ate/CreateAteFaultCode';
import BuildsSummaryReportOptions from '../containers/buildsByDepartment/BuildsSummaryReportOptions';
import BuildsSummaryReport from '../containers/buildsByDepartment/BuildsSummaryReport';
import ProductionMeasures from '../containers/reports/measures/ProductionMeasures';
import SerialNumberReissue from '../containers/serialNumberReissue/SerialNumberReissue';
import BuildsDetailReportOptions from '../containers/buildsByDepartment/BuildsDetailReportOptions';
import BuildsDetailReport from '../containers/buildsByDepartment/BuildsDetailReport';
import ManufacturingSkills from '../containers/manufacturingSkills/ManufacturingSkills';
import ManufacturingSkill from '../containers/manufacturingSkills/ManufacturingSkill';
import CreateManufacturingSkill from '../containers/manufacturingSkills/CreateManufacturingSkill';
import ManufacturingResources from '../containers/manufacturingResources/ManufacturingResources';
import ManufacturingResource from '../containers/manufacturingResources/ManufacturingResource';
import CreateManufacturingResource from '../containers/manufacturingResources/CreateManufacturingResource';
import BoardFailTypes from '../containers/boardFailTypes/BoardFailTypes';
import BoardFailType from '../containers/boardFailTypes/BoardFailType';
import CreateBoardFailType from '../containers/boardFailTypes/CreateBoardFailType';
import AssemblyFailsWaitingListReport from '../containers/reports/AssemblyFailsWaitingListReport';
import AssemblyFail from '../containers/assemblyFails/AssemblyFail';
import AssemblyFails from '../containers/assemblyFails/AssemblyFails';
import CreateAssemblyFail from '../containers/assemblyFails/CreateAssemblyFail';
import WhoBuiltWhatReportOptions from '../containers/reports/WhoBuiltWhatReportOptions';
import WhoBuiltWhatReport from '../containers/reports/WhoBuiltWhatReport';
import WhoBuiltWhatDetailsReport from '../containers/reports/WhoBuiltWhatDetailsReport';
import ProductionTriggersReport from '../containers/reports/triggers/ProductionTriggersReport';
import ProductionTriggersFacts from '../containers/reports/triggers/ProductionTriggersFacts';
import AssemblyFailsMeasuresOptions from '../containers/reports/AssemblyFailsMeasuresOptions';
import AssemblyFailsMeasures from '../containers/reports/AssemblyFailsMeasures';
import AssemblyFailsDetails from '../containers/reports/AssemblyFailsDetails';
import WorksOrder from '../containers/worksOrders/WorksOrder';
import CreateWorksOrder from '../containers/worksOrders/CreateWorksOrder';
import NotFound from './NotFound';
import SmtOutstandingWOPartsReportOptions from '../containers/reports/SmtOutstandingWOPartsReportOptions';
import SmtOutstandingWOPartsReport from '../containers/reports/SmtOutstandingWOPartsReport';
import PtlSettings from '../containers/productionTriggerLevels/PtlSettings';
import AssemblyFailFaultCodes from '../containers/assemblyFails/AssemblyFailFaultCodes';
import AssemblyFailFaultCode from '../containers/assemblyFails/AssemblyFailFaultCode';
import CreateAssemblyFailFaultCode from '../containers/assemblyFails/CreateAssemblyFailFaultCode';
import ManufacturingCommitDateReportOptions from '../containers/reports/ManufacturingCommitDateReportOptions';
import ManufacturingCommitDateReport from '../containers/reports/ManufacturingCommitDateReport';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
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
                                        path="/production/maintenance/signin-oidc-client"
                                        component={Callback}
                                    />

                                    <Route exact path="/production/maintenance" component={App} />

                                    <Route exact path="/production/quality" component={App} />

                                    <Route exact path="/production/quality/ate" component={App} />
                                    <Route
                                        exact
                                        path="/production/works-orders/outstanding-works-orders-report"
                                        component={OutstandingWorksOrdersReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/works-orders/outstanding-works-orders-report/report"
                                        component={OutstandingWorksOrdersReport}
                                    />

                                    <Route
                                        exact
                                        path="/production/works-orders"
                                        component={WorksOrder}
                                    />

                                    <Route
                                        exact
                                        path="/production/works-orders/create"
                                        component={CreateWorksOrder}
                                    />

                                    <Route
                                        exact
                                        path="/production/works-orders/:id"
                                        component={WorksOrder}
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
                                        path="/production/reports/builds-summary/options"
                                        component={BuildsSummaryReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/triggers/facts"
                                        component={ProductionTriggersFacts}
                                            />
                                    <Route
                                        exact
                                        path="/production/reports/triggers"
                                        component={ProductionTriggersReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/builds-summary"
                                        component={BuildsSummaryReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/builds-detail/options"
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
                                        path="/production/reports/measures"
                                        component={ProductionMeasures}
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
                                        path="/production/reports/who-built-what/report"
                                        component={WhoBuiltWhatReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/who-built-what"
                                        component={WhoBuiltWhatReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/who-built-what-details"
                                        component={WhoBuiltWhatDetailsReport}
                                    />

                                    <Route
                                        exact
                                        path="/production/quality/assembly-fails/:id"
                                        component={AssemblyFail}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/assembly-fails"
                                        component={AssemblyFails}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/create-assembly-fail"
                                        component={CreateAssemblyFail}
                                    />

                                    <Route
                                        exact
                                        path="/production/quality/assembly-fail-fault-codes"
                                        component={AssemblyFailFaultCodes}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/assembly-fail-fault-codes/:id"
                                        component={AssemblyFailFaultCode}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/assembly-fail-fault-codes/create"
                                        component={CreateAssemblyFailFaultCode}
                                    />

                                    <Route
                                        exact
                                        path="/production/resources/manufacturing-resources/create"
                                        component={CreateManufacturingResource}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/manufacturing-resources"
                                        component={ManufacturingResources}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/manufacturing-resources/:id"
                                        component={ManufacturingResource}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/assembly-fails-measures/report"
                                        component={AssemblyFailsMeasures}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/assembly-fails-measures"
                                        component={AssemblyFailsMeasuresOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/assembly-fails-details"
                                        component={AssemblyFailsDetails}
                                    />

                                    <Route
                                        exact
                                        path="/production/reports/smt/outstanding-works-order-parts/report"
                                        component={SmtOutstandingWOPartsReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/smt/outstanding-works-order-parts"
                                        component={SmtOutstandingWOPartsReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/production-trigger-levels-settings"
                                        component={PtlSettings}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/manufacturing-commit-date/report"
                                        component={ManufacturingCommitDateReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/manufacturing-commit-date"
                                        component={ManufacturingCommitDateReportOptions}
                                    />
                                    <Route component={NotFound} />
                                </Switch>
                            </div>
                        </Router>
                    </MuiPickersUtilsProvider>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
