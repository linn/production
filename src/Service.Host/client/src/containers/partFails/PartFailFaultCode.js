import { connect } from 'react-redux';
import {
    getItemError,
    getRequestErrors,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PartFailFaultCode from '../../components/partFails/PartFailFaultCode';
import partFailFaultCodeActions from '../../actions/partFailFaultCodeActions';
import partFailFaultCodeSelectors from '../../selectors/partFailFaultCodeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: partFailFaultCodeSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: partFailFaultCodeSelectors.getEditStatus(state),
    loading: partFailFaultCodeSelectors.getLoading(state),
    snackbarVisible: partFailFaultCodeSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.partFailFaultCode.item),
    requestErrors: getRequestErrors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(partFailFaultCodeActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: partFailFaultCodeActions.update,
    setEditStatus: partFailFaultCodeActions.setEditStatus,
    setSnackbarVisible: partFailFaultCodeActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartFailFaultCode));
