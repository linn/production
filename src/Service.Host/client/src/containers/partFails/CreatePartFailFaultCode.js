import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partFailFaultCodeActions from '../../actions/partFailFaultCodeActions';
import PartFailFaultCode from '../../components/partFails/PartFailFaultCode';
import partFailFaultCodeSelectors from '../../selectors/partFailFaultCodeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.partFailFaultCode.item),
    loading: partFailFaultCodeSelectors.getLoading(state),
    snackbarVisible: partFailFaultCodeSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(partFailFaultCodeActions.setEditStatus('create'));
    dispatch(partFailFaultCodeActions.clearErrorsForItem());
};

const mapDispatchToProps = {
    initialise,
    addItem: partFailFaultCodeActions.add,
    setEditStatus: partFailFaultCodeActions.setEditStatus,
    setSnackbarVisible: partFailFaultCodeActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartFailFaultCode));
