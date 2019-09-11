import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingRoute from '../../components/manufacturingRoutes/ManufacturingRoute';
import manufacturingRouteActions from '../../actions/manufacturingRouteActions';
import manufacturingRouteSelectors from '../../selectors/manufacturingRouteSelectors';

const mapStateToProps = (state, { match }) => ({
    item: manufacturingRouteSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: manufacturingRouteSelectors.getEditStatus(state),
    loading: manufacturingRouteSelectors.getLoading(state),
    snackbarVisible: manufacturingRouteSelectors.getSnackbarVisible(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(manufacturingRouteActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: manufacturingRouteActions.update,
    setEditStatus: manufacturingRouteActions.setEditStatus,
    setSnackbarVisible: manufacturingRouteActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingRoute));
