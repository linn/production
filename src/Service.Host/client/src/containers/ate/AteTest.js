import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AteTest from '../../components/ate/AteTest';
import ateTestActions from '../../actions/ateTestActions';
import ateTestSelectors from '../../selectors/ateTestSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: ateTestSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: ateTestSelectors.getEditStatus(state),
    loading: ateTestSelectors.getLoading(state),
    snackbarVisible: ateTestSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.ateTest.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(ateTestActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: ateTestActions.update,
    setEditStatus: ateTestActions.setEditStatus,
    setSnackbarVisible: ateTestActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(AteTest));
