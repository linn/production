import { connect } from 'react-redux';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import OutstandingWorksOrdersReport from '../../components/reports/OutstandingWorksOrdersReport';
import initialiseOnMount from '../common/initialiseOnMount';
import actions from '../../actions/outstandingWorksOrdersReport';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.outstandingWorksOrdersReport.item);

const mapStateToProps = state => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    config
});

const initialise = () => dispatch => {
    dispatch(actions.fetchReport());
};

const mapDispatchToProps = {
    initialise,
    fetchReport: actions.fetchReport()
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(OutstandingWorksOrdersReport));
