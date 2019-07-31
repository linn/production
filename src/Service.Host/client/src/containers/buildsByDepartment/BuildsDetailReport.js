import { connect } from 'react-redux';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import initialiseOnMount from '../initialiseOnMount';
import BuildsDetailReport from '../../components/buildsbyDepartment/BuildsDetailReport';
import actions from '../../actions/buildsDetailReportActions';
import config from '../../config';

const reportSelectors = new ReportSelectors('buildsDetailReport');

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    if (options.fromDate === 'undefined') {
        options.fromDate = new Date().toISOString();
    }
    if (options.toDate === 'undefined') {
        options.toDate = new Date().toISOString();
    }
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

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(BuildsDetailReport));
