import { connect } from 'react-redux';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import BuildsDetailReport from '../../components/buildsbyDepartment/BuildsDetailReport';
import actions from '../../actions/buildsDetailReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.buildsDetailReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    options: getOptions(ownProps),
    config
});

const initialise = props => dispatch => {
    dispatch(actions.fetchReport(props.options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(BuildsDetailReport));
