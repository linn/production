import { connect } from 'react-redux';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AssemblyFailsWaitingList from '../../components/reports/AssemblyFailsWaitingListReport';
import actions from '../../actions/AssemblyFailsWaitingListReport';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.AssemblyFailsWaitingListReport.item);

const mapStateToProps = state => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    config
});

const initialise = () => dispatch => {
    dispatch(actions.fetchReport());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AssemblyFailsWaitingList));
