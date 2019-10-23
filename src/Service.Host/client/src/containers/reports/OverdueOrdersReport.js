import { connect } from 'react-redux';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import initialiseOnMount from '../initialiseOnMount';
import OverdueOrderReport from '../../components/reports/OverdueOrdersReport';
import actions from '../../actions/overdueOrdersReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.overdueOrders.item);

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

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(OverdueOrderReport));
