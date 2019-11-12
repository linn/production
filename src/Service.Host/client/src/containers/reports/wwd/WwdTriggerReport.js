import { connect } from 'react-redux';
import { ReportSelectors, getItemError } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import initialiseOnMount from '../../initialiseOnMount';
import WwdTriggerReport from '../../../components/reports/wwd/WwdTriggerReport';
import actions from '../../../actions/wwdTriggerReportActions';
import * as reportTypes from '../../../reportTypes';

import config from '../../../config';

const reportSelectors = new ReportSelectors(reportTypes.wwdTriggerReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options || {};
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    options: getOptions(ownProps),
    itemError: getItemError(state, reportTypes.productionTriggerFacts.item),
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
)(initialiseOnMount(WwdTriggerReport));
