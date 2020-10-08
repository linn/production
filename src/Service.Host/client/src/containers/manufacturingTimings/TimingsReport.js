import { connect } from 'react-redux';
import {
    ReportSelectors,
    initialiseOnMount,
    getItemError
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import TimingsReport from '../../components/manufacturingTimings/TimingsReport';
import actions from '../../actions/manufacturingTimingsReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.manufacturingTimingsReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    options: getOptions(ownProps),
    config,
    itemError: getItemError(state, reportTypes.manufacturingTimingsReport.item)
});

const initialise = props => dispatch => {
    dispatch(actions.fetchReport(props.options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(TimingsReport));
