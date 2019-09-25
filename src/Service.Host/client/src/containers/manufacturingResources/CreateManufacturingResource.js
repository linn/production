import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingResource from '../../components/manufacturingResources/ManufacturingResource';
import manufacturingResourceActions from '../../actions/manufacturingResourceActions';
import manufacturingResourceSelectors from '../../selectors/manufacturingResourceSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.manufacturingResource.item),
    loading: manufacturingResourceSelectors.getLoading(state),
    snackbarVisible: manufacturingResourceSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(manufacturingResourceActions.setEditStatus('create'));
    dispatch(manufacturingResourceActions.clearErrorsForItem());
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
