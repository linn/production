import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partFailErrorTypeActions from '../../actions/partFailErrorTypeActions';
import PartFailErrorType from '../../components/partFails/PartFailErrorType';
import partFailErrorTypeSelectors from '../../selectors/partFailErrorTypeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.partFailErrorType.item),
    loading: partFailErrorTypeSelectors.getLoading(state),
    snackbarVisible: partFailErrorTypeSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(partFailErrorTypeActions.setEditStatus('create'));
    dispatch(partFailErrorTypeActions.clearErrorsForItem());
};

const mapDispatchToProps = {
    initialise,
    addItem: partFailErrorTypeActions.add,
    setEditStatus: partFailErrorTypeActions.setEditStatus,
    setSnackbarVisible: partFailErrorTypeActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartFailErrorType));
