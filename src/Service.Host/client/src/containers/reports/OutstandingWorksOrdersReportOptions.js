import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import OutstandingWorksOrdersReportOptions from '../../components/reports/OutstandingWorksOrdersReportOptions';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';

const mapStateToProps = state => ({
    cits: citsSelectors.getItems(state),
    citsLoading: citsSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchCits: citsActions.fetch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(OutstandingWorksOrdersReportOptions)));
