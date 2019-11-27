import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import LabelType from '../../components/labelTypes/LabelType';
import labelTypeActions from '../../actions/labelTypeActions';
import labelTypeSelectors from '../../selectors/labelTypeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: labelTypeSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: labelTypeSelectors.getEditStatus(state),
    loading: labelTypeSelectors.getLoading(state),
    snackbarVisible: labelTypeSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.labelType.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(labelTypeActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: labelTypeActions.update,
    setEditStatus: labelTypeActions.setEditStatus,
    setSnackbarVisible: labelTypeActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LabelType));
