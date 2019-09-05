import { connect } from 'react-redux';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import initialiseOnMount from '../../initialiseOnMount';
import ProductionTriggers from '../../../components/reports/triggers/ProductionTriggers';
import actions from '../../../actions/productionTriggersReport';
import * as reportTypes from '../../../reportTypes';

import config from '../../../config';

const reportSelectors = new ReportSelectors(reportTypes.productionTriggersReport.item);

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

const initialise = props => dispatch => {
    console.log(reportTypes.productionTriggersReport.item);
    dispatch(actions.fetchReport(props.options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ProductionTriggers));
