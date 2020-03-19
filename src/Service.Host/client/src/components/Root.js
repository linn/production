﻿import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { ConnectedRouter } from 'connected-react-router';
import { OidcProvider } from 'redux-oidc';
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
import WwdTriggerReport from '../containers/reports/wwd/WwdTriggerReport';
import AssemblyFailsMeasuresOptions from '../containers/reports/AssemblyFailsMeasuresOptions';
import AssemblyFailsMeasures from '../containers/reports/AssemblyFailsMeasures';
import AssemblyFailsDetailsOptions from '../containers/reports/AssemblyFailsDetailsOptions';
import AssemblyFailsDetails from '../containers/reports/AssemblyFailsDetails';
import WorksOrder from '../containers/worksOrders/WorksOrder';
import CreateWorksOrder from '../containers/worksOrders/CreateWorksOrder';
import NotFound from './NotFound';
import SmtOutstandingWOPartsReportOptions from '../containers/reports/SmtOutstandingWOPartsReportOptions';
import SmtOutstandingWOPartsReport from '../containers/reports/SmtOutstandingWOPartsReport';
import BoardTestsReportOptions from '../containers/reports/BoardTestsReportOptions';
import BoardTestsReport from '../containers/reports/BoardTestsReport';
import BoardTestDetailsReport from '../containers/reports/BoardTestDetailsReport';
import PtlSettings from '../containers/productionTriggerLevels/PtlSettings';
import PartFail from '../containers/partFails/PartFail';
import PartFails from '../containers/partFails/PartFails';
import CreatePartFail from '../containers/partFails/CreatePartFail';
import AssemblyFailFaultCodes from '../containers/assemblyFails/AssemblyFailFaultCodes';
import AssemblyFailFaultCode from '../containers/assemblyFails/AssemblyFailFaultCode';
import CreateAssemblyFailFaultCode from '../containers/assemblyFails/CreateAssemblyFailFaultCode';
import ManufacturingCommitDateReportOptions from '../containers/reports/ManufacturingCommitDateReportOptions';
import ManufacturingCommitDateReport from '../containers/reports/ManufacturingCommitDateReport';
import OverdueOrdersReportOptions from '../containers/reports/OverdueOrdersReportOptions';
import OverdueOrdersReport from '../containers/reports/OverdueOrdersReport';
import PartFailErrorTypes from '../containers/partFails/PartFailErrorTypes';
import PartFailFaultCodes from '../containers/partFails/PartFailFaultCodes';
import PartFailErrorType from '../containers/partFails/PartFailErrorType';
import CreatePartFailErrorType from '../containers/partFails/CreatePartFailErrorType';
import CreatePartFailFaultCode from '../containers/partFails/CreatePartFailFaultCode';
import PartFailFaultCode from '../containers/partFails/PartFailFaultCode';
import WorksOrderBatchNotes from '../containers/worksOrders/WorksOrderBatchNotes';
import ManufacturingRoute from '../containers/manufacturingRoutes/ManufacturingRoute';
import ManufacturingRoutes from '../containers/manufacturingRoutes/ManufacturingRoutes';
import CreateManufacturingRoute from '../containers/manufacturingRoutes/CreateManufacturingRoute';
import PartFailDetailsReportOptions from '../containers/reports/PartFailDetailsReportOptions';
import PartFailDetailsReport from '../containers/reports/PartFailDetailsReport';
import WorksOrderLabels from '../containers/worksOrders/WorksOrderLabels';
import WorksOrderLabel from '../containers/worksOrders/WorksOrderLabel';
import CreateWorksOrderLabel from '../containers/worksOrders/CreateWorksOrderLabel';
import ProductionBackOrdersReport from '../containers/reports/ProductionBackOrdersReport';
import BuildPlansReportOptions from '../containers/reports/BuildPlansReportOptions';
import BuildPlansReport from '../containers/reports/BuildPlansReport';
import LabelTypes from '../containers/labelTypes/LabelTypes';
import LabelType from '../containers/labelTypes/LabelType';
import CreateLabelType from '../containers/labelTypes/CreateLabelType';
import LabelReprint from '../containers/labelReprints/LabelReprint';
import CreateLabelReprint from '../containers/labelReprints/CreateLabelReprint';
import ProductionTriggerLevels from '../containers/productionTriggerLevels/ProductionTriggerLevels';
import ProductionTriggerLevel from '../containers/productionTriggerLevels/ProductionTriggerLevel';
import CreateProductionTriggerLevel from '../containers/productionTriggerLevels/CreateProductionTriggerLevel';
import BuildPlans from '../containers/buildPlans/BuildPlans';
import CreateBuildPlan from '../containers/buildPlans/CreateBuildPlan';
import AteStatusReportOptions from '../containers/reports/AteStatusReportOptions';
import AteStatusReport from '../containers/reports/AteStatusReport';
import AteDetailsReport from '../containers/reports/AteDetailsReport';
import AteTest from '../containers/ate/AteTest';
import AteTests from '../containers/ate/AteTests';
import CreateAteTest from '../containers/ate/CreateAteTest';
import BtwReport from '../containers/reports/BtwReport';
import DelPerfReport from '../containers/reports/DelPerfReport';
import DelPerfDetails from '../containers/reports/DelPerfDetails';
import ShortageSummary from '../containers/reports/ShortageSummary';
import LabelPrinter from '../containers/labelPrinting/LabelPrint';
import FailedPartsReport from '../containers/reports/FailedPartsReport';
import LabelPrint from '../containers/labels/LabelPrint';
import PurchaseOrder from '../containers/PurchaseOrder';
import PurchaseOrders from '../containers/PurchaseOrders';
import DaysRequiredReport from '../containers/reports/DaysRequiredReport';
import manufacturingTimingsReport from '../containers/manufacturingTimings/TimingsReport';
import manufacturingTimingsSetup from '../containers/manufacturingTimings/TimingsSetup';
import MechPartSource from '../containers/mechPartSource/MechPartSource';

const Root = ({ store }) => (
    <div>
        <div className="padding-top-when-not-printing">
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <MuiPickersUtilsProvider utils={MomentUtils}>
                        <ConnectedRouter history={history}>
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
                                        path="/production/works-orders/batch-notes"
                                        component={WorksOrderBatchNotes}
                                    />
                                    <Route
                                        exact
                                        path="/production/works-orders/labels/create"
                                        component={CreateWorksOrderLabel}
                                    />
                                    <Route
                                        exact
                                        path="/production/works-orders/labels"
                                        component={WorksOrderLabels}
                                    />
                                    <Route
                                        exact
                                        path="/production/works-orders/labels/:id+"
                                        component={WorksOrderLabel}
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
                                        path="/production/reports/wwd"
                                        component={WwdTriggerReport}
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
                                        path="/production/maintenance/labels/reprint-reasons/create"
                                        component={CreateLabelReprint}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/labels/reprint-reasons/:id"
                                        component={LabelReprint}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/labels/reprint-reasons"
                                        render={() => (
                                            <Redirect to="/production/maintenance/labels/reprint-reasons/create" />
                                        )}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/labels"
                                        render={() => <Redirect to="/production/maintenance/" />}
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
                                        path="/production/quality/assembly-fails"
                                        component={AssemblyFails}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/assembly-fails/create"
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
                                        path="/production/quality/assembly-fails/:id"
                                        component={AssemblyFail}
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
                                        path="/production/reports/ate/status/report"
                                        component={AteStatusReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/ate/status"
                                        component={AteStatusReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/ate/details/report"
                                        component={AteDetailsReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/ate"
                                        render={() => <Redirect to="/production/maintenance" />}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/assembly-fails-details/report"
                                        component={AssemblyFailsDetails}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/assembly-fails-details"
                                        component={AssemblyFailsDetailsOptions}
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
                                        path="/production/reports/board-tests-report/report"
                                        component={BoardTestsReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/board-tests-report"
                                        component={BoardTestsReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/board-test-details-report"
                                        component={BoardTestDetailsReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/production-trigger-levels-settings"
                                        component={PtlSettings}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fails"
                                        component={PartFails}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fails/create"
                                        component={CreatePartFail}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fails/detail-report/report"
                                        component={PartFailDetailsReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fails/detail-report"
                                        component={PartFailDetailsReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fails/:id"
                                        component={PartFail}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fail-error-types"
                                        component={PartFailErrorTypes}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fail-error-types/create"
                                        component={CreatePartFailErrorType}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fail-error-types/:id"
                                        component={PartFailErrorType}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fail-fault-codes"
                                        component={PartFailFaultCodes}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fail-fault-codes/create"
                                        component={CreatePartFailFaultCode}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/part-fail-fault-codes/:id"
                                        component={PartFailFaultCode}
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
                                    <Route
                                        exact
                                        path="/production/maintenance/labels/reprint"
                                        component={LabelPrint}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/overdue-orders/report"
                                        component={OverdueOrdersReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/overdue-orders"
                                        component={OverdueOrdersReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/production-back-orders"
                                        component={ProductionBackOrdersReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/manufacturing-routes/create"
                                        component={CreateManufacturingRoute}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/manufacturing-routes/:id"
                                        component={ManufacturingRoute}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/manufacturing-routes"
                                        component={ManufacturingRoutes}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/build-plans/report"
                                        component={BuildPlansReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/build-plans"
                                        component={BuildPlansReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/label-types/create"
                                        component={CreateLabelType}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/label-types/:id"
                                        component={LabelType}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/label-types"
                                        component={LabelTypes}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/production-trigger-levels/create"
                                        component={CreateProductionTriggerLevel}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/production-trigger-levels/:id"
                                        component={ProductionTriggerLevel}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/production-trigger-levels"
                                        component={ProductionTriggerLevels}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/build-plans"
                                        component={BuildPlans}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/build-plans/create"
                                        component={CreateBuildPlan}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/build-plans/:id"
                                        component={BuildPlans}
                                    />

                                    <Route
                                        path="/production/maintenance/build-plan-details"
                                        component={BuildPlans}
                                    />

                                    <Route
                                        exact
                                        path="/production/quality/ate-tests/create"
                                        component={CreateAteTest}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/ate-tests/:id"
                                        component={AteTest}
                                    />
                                    <Route
                                        exact
                                        path="/production/quality/ate-tests"
                                        component={AteTests}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/btw"
                                        component={BtwReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/delperf"
                                        component={DelPerfReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/delperf/details"
                                        component={DelPerfDetails}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/shortages"
                                        component={ShortageSummary}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/labels/print"
                                        component={LabelPrinter}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/failed-parts"
                                        component={FailedPartsReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/purchase-orders/:id"
                                        component={PurchaseOrder}
                                    />
                                    <Route
                                        exact
                                        path="/production/resources/purchase-orders"
                                        component={PurchaseOrders}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/days-required"
                                        component={DaysRequiredReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/manufacturing-timings-setup"
                                        component={manufacturingTimingsSetup}
                                    />
                                    <Route
                                        exact
                                        path="/production/reports/manufacturing-timings"
                                        component={manufacturingTimingsReport}
                                    />
                                    <Route
                                        exact
                                        path="/production/maintenance/mech-part-source"
                                        component={MechPartSource}
                                    />
                                    <Route component={NotFound} />
                                </Switch>
                            </div>
                        </ConnectedRouter>
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
