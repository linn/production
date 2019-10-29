import { connect } from 'react-redux';
import {
    getItemError,
    getRequestErrors,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import partFailErrorType from '../../components/partFails/PartFailErrorType';
import partFailErrorTypeActions from '../../actions/partFailErrorTypeActions';
import partFailErrorTypeSelectors from '../../selectors/partFailErrorTypeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: partFailErrorTypeSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: partFailErrorTypeSelectors.getEditStatus(state),
    loading: partFailErrorTypeSelectors.getLoading(state),
    snackbarVisible: partFailErrorTypeSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.partFailErrorType.item),
    requestErrors: getRequestErrors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(partFailErrorTypeActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: partFailErrorTypeActions.update,
    setEditStatus: partFailErrorTypeActions.setEditStatus,
    setSnackbarVisible: partFailErrorTypeActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(partFailErrorType));
