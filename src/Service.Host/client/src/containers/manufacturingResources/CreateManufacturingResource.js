import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingResource from '../../components/manufacturingResources/ManufacturingResource';
import manufacturingResourceActions from '../../actions/manufacturingResourceActions';
import manufacturingResourceSelectors from '../../selectors/manufacturingResourceSelectors';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    errorMessage: fetchErrorSelectors(state),
    loading: manufacturingResourceSelectors.getLoading(state),
    snackbarVisible: manufacturingResourceSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(manufacturingResourceActions.setEditStatus('create'));
    dispatch(manufacturingResourceActions.create());
};

const mapDispatchToProps = {
    initialise,
    addItem: manufacturingResourceActions.add,
    setEditStatus: manufacturingResourceActions.setEditStatus,
    setSnackbarVisible: manufacturingResourceActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingResource));
