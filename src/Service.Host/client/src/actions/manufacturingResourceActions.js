import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { manufacturingResourceActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.manufacturingResource.actionType,
    itemTypes.manufacturingResource.uri,
    actionTypes,
    config.appRoot
);
