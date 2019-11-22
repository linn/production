import { connect } from 'react-redux';
import {
    getItemError,
    getRequestErrors,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import BoardFailType from '../../components/boardFailTypes/BoardFailType';
import boardFailTypeActions from '../../actions/boardFailTypeActions';
import boardFailTypeSelectors from '../../selectors/boardFailTypeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: boardFailTypeSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: boardFailTypeSelectors.getEditStatus(state),
    loading: boardFailTypeSelectors.getLoading(state),
    snackbarVisible: boardFailTypeSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.boardFailType.item),
    requestErrors: getRequestErrors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(boardFailTypeActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: boardFailTypeActions.update,
    setEditStatus: boardFailTypeActions.setEditStatus,
    setSnackbarVisible: boardFailTypeActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(BoardFailType));
