import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import LabelReprint from '../../components/labelReprints/LabelReprint';
import labelReprintActions from '../../actions/labelReprintActions';
import labelReprintSelectors from '../../selectors/labelReprintSelectors';
import * as itemTypes from '../../itemTypes';
import labelTypesActions from '../../actions/labelTypesActions';
import labelTypesSelectors from '../../selectors/labelTypesSelectors';

const mapStateToProps = (state, { match }) => ({
    item: labelReprintSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: labelReprintSelectors.getEditStatus(state),
    loading: labelReprintSelectors.getLoading(state),
    snackbarVisible: labelReprintSelectors.getSnackbarVisible(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.labelReprint.item),
    labelTypes: labelTypesSelectors.getItems(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(labelReprintActions.fetch(itemId));
    dispatch(labelTypesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: labelReprintActions.update,
    setEditStatus: labelReprintActions.setEditStatus,
    setSnackbarVisible: labelReprintActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LabelReprint));
