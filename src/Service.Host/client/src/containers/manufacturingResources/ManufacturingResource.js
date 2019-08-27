﻿import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingResource from '../../components/manufacturingResources/ManufacturingResource';
import manufacturingResourceActions from '../../actions/manufacturingResourceActions';
import manufacturingResourceSelectors from '../../selectors/manufacturingResourceSelectors';

const mapStateToProps = (state, { match }) => ({
    item: manufacturingResourceSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: manufacturingResourceSelectors.getEditStatus(state),
    loading: manufacturingResourceSelectors.getLoading(state),
    snackbarVisible: manufacturingResourceSelectors.getSnackbarVisible(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(manufacturingResourceActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: manufacturingResourceActions.update,
    setEditStatus: manufacturingResourceActions.setEditStatus,
    setSnackbarVisible: manufacturingResourceActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingResource));
