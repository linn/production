import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import BoardFailType from '../../components/boardFailTypes/BoardFailType';
import boardFailTypeActions from '../../actions/boardFailTypeActions';
import boardFailTypeSelectors from '../../selectors/boardFailTypeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.boardFailType.item),
    loading: boardFailTypeSelectors.getLoading(state),
    snackbarVisible: boardFailTypeSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(boardFailTypeActions.setEditStatus('create'));
    dispatch(boardFailTypeActions.clearErrorsForItem());
};

const mapDispatchToProps = {
    initialise,
    addItem: boardFailTypeActions.add,
    setEditStatus: boardFailTypeActions.setEditStatus,
    setSnackbarVisible: boardFailTypeActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(BoardFailType));
