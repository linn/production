import { connect } from 'react-redux';
import { ReportSelectors, getItemError } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import initialiseOnMount from '../initialiseOnMount';
import BuildsSummaryReport from '../../components/buildsbyDepartment/BuildsSummaryReport';
import actions from '../../actions/buildsSummaryReport';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors('buildsSummaryReport');

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    options: getOptions(ownProps),
    itemError: getItemError(state, reportTypes.buildsSummaryReport),
    config
});

const initialise = props => dispatch => {
    dispatch(actions.fetchReport(props.options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(BuildsSummaryReport));
