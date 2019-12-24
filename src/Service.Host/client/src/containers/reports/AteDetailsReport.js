import { connect } from 'react-redux';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import AteDetailsReport from '../../components/reports/AteDetailsReport';
import actions from '../../actions/ateDetailsReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.ateDetailsReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options || {};
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    options: getOptions(ownProps),
    config
});

const initialise = ({ options }) => dispatch => {
    dispatch(actions.fetchReport(options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(AteDetailsReport));
