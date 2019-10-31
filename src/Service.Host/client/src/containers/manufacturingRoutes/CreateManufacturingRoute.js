import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingRoute from '../../components/manufacturingRoutes/ManufacturingRoute';
import manufacturingRouteActions from '../../actions/manufacturingRouteActions';
import manufacturingRouteSelectors from '../../selectors/manufacturingRouteSelectors';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    errorMessage: fetchErrorSelectors(state),
    loading: manufacturingRouteSelectors.getLoading(state),
    snackbarVisible: manufacturingRouteSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(manufacturingRouteActions.setEditStatus('create'));
    dispatch(manufacturingRouteActions.create());
};

const mapDispatchToProps = {
    initialise,
    addItem: manufacturingRouteActions.add,
    setEditStatus: manufacturingRouteActions.setEditStatus,
    setSnackbarVisible: manufacturingRouteActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingRoute));
