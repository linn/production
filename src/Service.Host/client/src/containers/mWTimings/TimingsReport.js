import { connect } from 'react-redux';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import TimingsReport from '../../components/mWTimings/TimingsReport';
import actions from '../../actions/metalWorkTimingsReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.metalWorkTimingsReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => {
    console.info(reportTypes.metalWorkTimingsReport);
    return {
        reportData: reportSelectors.getReportData(state),
        loading: reportSelectors.getReportLoading(state),
        options: getOptions(ownProps),
        config
    };
};

const initialise = props => dispatch => {
    dispatch(actions.fetchReport(props.options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(TimingsReport));
