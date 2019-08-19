import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import BoardFailType from '../../components/boardFailTypes/BoardFailType';
import boardFailTypeActions from '../../actions/boardFailTypeActions';
import boardFailTypeSelectors from '../../selectors/boardFailTypeSelectors';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    errorMessage: fetchErrorSelectors(state),
    loading: boardFailTypeSelectors.getLoading(state),
    snackbarVisible: boardFailTypeSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(boardFailTypeActions.setEditStatus('create'));
    dispatch(boardFailTypeActions.create());
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
