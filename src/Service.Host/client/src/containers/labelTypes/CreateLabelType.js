import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import LabelType from '../../components/labelTypes/LabelType';
import labelTypeActions from '../../actions/labelTypeActions';
import labelTypeSelectors from '../../selectors/labelTypeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: labelTypeSelectors.getEditStatus(state),
    itemError: getItemError(state, itemTypes.labelType.item),
    loading: labelTypeSelectors.getLoading(state),
    snackbarVisible: labelTypeSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(labelTypeActions.clearErrorsForItem());
    dispatch(labelTypeActions.setEditStatus('create'));
};

const mapDispatchToProps = {
    initialise,
    addItem: labelTypeActions.add,
    setEditStatus: labelTypeActions.setEditStatus,
    setSnackbarVisible: labelTypeActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LabelType));
