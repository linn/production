import { connect } from 'react-redux';
import {
    ReportSelectors,
    initialiseOnMount,
    getItemErrorDetailMessage
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import ShortageSummary from '../../components/reports/ShortageSummary';
import actions from '../../actions/shortageSummaryActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.shortageSummary.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options || {};
};

const mapStateToProps = (state, ownProps) => ({
    summary: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    options: getOptions(ownProps),
    error: getItemErrorDetailMessage(state, reportTypes.shortageSummary.item),
    config
});

const initialise = ({ options }) => dispatch => {
    dispatch(actions.fetchReport(options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ShortageSummary));