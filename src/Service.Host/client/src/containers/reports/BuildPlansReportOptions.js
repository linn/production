import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import BuildPlansReportOptions from '../../components/reports/BuildPlansReportOptions';
import citsActions from '../../actions/citsActions';
import buildPlansActions from '../../actions/buildPlansActions';
import citsSelectors from '../../selectors/citsSelectors';
import buildPlansSelectors from '../../selectors/buildPlansSelectors';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.buildPlansReport.item);

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state),
    buildPlans: buildPlansSelectors.getItems(state),
    buildPlansLoading: buildPlansSelectors.getLoading(state),
    cits: citsSelectors.getItems(state),
    citsLoading: citsSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(buildPlansActions.fetch());
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(BuildPlansReportOptions)));
