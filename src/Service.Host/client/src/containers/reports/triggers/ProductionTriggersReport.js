import { connect } from 'react-redux';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import initialiseOnMount from '../../initialiseOnMount';
import ProductionTriggers from '../../../components/reports/triggers/ProductionTriggers';
import actions from '../../../actions/productionTriggersReport';
import citsActions from '../../../actions/citsActions';
import citsSelectors from '../../../selectors/citsSelectors';
import * as reportTypes from '../../../reportTypes';

import config from '../../../config';

const reportSelectors = new ReportSelectors(reportTypes.productionTriggersReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options || {};
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state) || citsSelectors.getLoading(state),
    options: getOptions(ownProps),
    cits: citsSelectors.getItems(state),
    config
});

const initialise = props => dispatch => {
    dispatch(actions.fetchReport(props.options));
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchCits: actions.fetchReport
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ProductionTriggers));
